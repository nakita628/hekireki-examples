defmodule Rallly.Vote do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          participant: Rallly.Participant.t() | nil,
          option: Rallly.Option.t() | nil,
          poll: Rallly.Poll.t() | nil
        }

  schema "votes" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:yes, :no, :ifNeedBe], default: :yes)
    belongs_to(:participant, Rallly.Participant, foreign_key: :participant_id, type: :string)
    belongs_to(:option, Rallly.Option, foreign_key: :option_id, type: :string)
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end