defmodule Rallly.Option do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          start_time: DateTime.t(),
          duration: integer(),
          inserted_at: DateTime.t(),
          poll: Rallly.Poll.t() | nil,
          votes: [Rallly.Vote.t()]
        }

  schema "options" do
    field(:id, :string, primary_key: true)
    field(:start_time, Rallly.PrismaDateTime)
    field(:duration, :integer, default: 0, source: :duration_minutes)
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    has_many(:votes, Rallly.Vote, foreign_key: :option_id, references: :id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end