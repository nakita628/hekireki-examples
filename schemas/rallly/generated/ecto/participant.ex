defmodule Rallly.Participant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          email: String.t() | nil,
          note: String.t() | nil,
          token: String.t(),
          locale: String.t() | nil,
          time_zone: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          poll: Rallly.Poll.t() | nil,
          user: Rallly.User.t() | nil,
          invite: Rallly.PollInvite.t() | nil,
          votes: [Rallly.Vote.t()]
        }

  schema "participants" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:note, :string)
    field(:token, :string)
    field(:locale, :string)
    field(:time_zone, :string)
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    has_one(:invite, Rallly.PollInvite, foreign_key: :participant_id)
    has_many(:votes, Rallly.Vote, foreign_key: :participant_id)
    timestamps(inserted_at_source: :created_at)
  end
end