defmodule Typebot.SetVariableHistoryItem do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          result_id: String.t(),
          index: integer(),
          variable_id: String.t(),
          block_id: String.t(),
          block_index: integer() | nil,
          value: map(),
          result: Typebot.Result.t() | nil
        }

  schema "SetVariableHistoryItem" do
    field(:index, :integer, primary_key: true)
    field(:variable_id, :string, source: :variableId)
    field(:block_id, :string, source: :blockId)
    field(:block_index, :integer, source: :blockIndex)
    field(:value, :map)
    field(:result_id, :string, primary_key: true, source: :resultId)
    belongs_to(:result, Typebot.Result, foreign_key: :result_id, define_field: false, type: :string)
  end
end