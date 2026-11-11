defmodule LiteLLM.LiteLLMLens do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          version: integer(),
          data: map(),
          due_at: DateTime.t() | nil,
          reviews: [LiteLLM.LiteLLMLensReview.t()]
        }

  schema "LiteLLM_Lens" do
    field(:id, :string, primary_key: true)
    field(:version, :integer, default: 0)
    field(:data, :map)
    field(:due_at, LiteLLM.PrismaDateTime, read_after_writes: true)
    has_many(:reviews, LiteLLM.LiteLLMLensReview, foreign_key: :lens_id, references: :id)
  end
end