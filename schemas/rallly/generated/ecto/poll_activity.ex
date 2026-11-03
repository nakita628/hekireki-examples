defmodule Rallly.PollActivity do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          user_id: String.t() | nil,
          participant_id: String.t() | nil,
          invite_id: String.t() | nil,
          option_id: String.t() | nil,
          payload: map(),
          inserted_at: DateTime.t(),
          poll: Rallly.Poll.t() | nil
        }

  schema "poll_activities" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:user_id, :string)
    field(:participant_id, :string)
    field(:invite_id, :string)
    field(:option_id, :string)
    field(:payload, :map, default: %{})
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end