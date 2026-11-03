defmodule Rallly.PollInvite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          token: String.t(),
          opened_at: DateTime.t() | nil,
          reminded_at: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          poll: Rallly.Poll.t() | nil,
          participant: Rallly.Participant.t() | nil
        }

  schema "poll_invites" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:token, :string)
    field(:opened_at, Rallly.PrismaDateTime)
    field(:reminded_at, Rallly.PrismaDateTime)
    field(:revoked_at, Rallly.PrismaDateTime)
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    belongs_to(:participant, Rallly.Participant, foreign_key: :participant_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end