defmodule Typebot.VisitedEdge do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          result_id: String.t(),
          edge_id: String.t(),
          index: integer(),
          result: Typebot.Result.t() | nil
        }

  schema "VisitedEdge" do
    field(:edge_id, :string, source: :edgeId)
    field(:index, :integer, primary_key: true)
    field(:result_id, :string, primary_key: true, source: :resultId)
    belongs_to(:result, Typebot.Result, foreign_key: :result_id, define_field: false, type: :string)
  end
end