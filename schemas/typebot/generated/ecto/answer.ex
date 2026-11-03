defmodule Typebot.Answer do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          result_id: String.t(),
          block_id: String.t(),
          group_id: String.t(),
          variable_id: String.t() | nil,
          content: String.t(),
          updated_at: DateTime.t(),
          result: Typebot.Result.t() | nil
        }

  schema "Answer" do
    field(:block_id, :string, primary_key: true, source: :blockId)
    field(:group_id, :string, primary_key: true, source: :groupId)
    field(:variable_id, :string, source: :variableId)
    field(:content, :string)
    field(:result_id, :string, primary_key: true, source: :resultId)
    belongs_to(:result, Typebot.Result, foreign_key: :result_id, define_field: false, type: :string)
    timestamps(inserted_at: false, updated_at_source: :createdAt)
  end
end