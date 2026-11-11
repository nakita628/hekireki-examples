defmodule LiteLLM.LiteLLMAutoRouterBaselineObservation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          request_id: String.t(),
          scope: String.t(),
          started_at: float(),
          revision: integer(),
          data: String.t(),
          publication: String.t() | nil,
          conflicted: boolean()
        }

  schema "LiteLLM_AutoRouterBaselineObservation" do
    field(:request_id, :string, primary_key: true)
    field(:scope, :string)
    field(:started_at, :float)
    field(:revision, :integer)
    field(:data, :string)
    field(:publication, :string)
    field(:conflicted, :boolean, default: false)
  end
end