defmodule LiteLLM.LiteLLMGuardrailsTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :guardrail_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          guardrail_name: String.t(),
          litellm_params: map(),
          guardrail_info: map() | nil,
          team_id: String.t() | nil,
          status: String.t(),
          submitted_at: DateTime.t() | nil,
          reviewed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_GuardrailsTable" do
    field(:guardrail_name, :string)
    field(:litellm_params, :map)
    field(:guardrail_info, :map)
    field(:team_id, :string)
    field(:status, :string, default: "active")
    field(:submitted_at, LiteLLM.PrismaDateTime)
    field(:reviewed_at, LiteLLM.PrismaDateTime)
    timestamps(inserted_at_source: :created_at)
  end
end