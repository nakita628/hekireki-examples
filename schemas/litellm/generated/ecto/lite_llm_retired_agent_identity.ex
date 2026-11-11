defmodule LiteLLM.LiteLLMRetiredAgentIdentity do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :binding_id}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          provider: String.t(),
          issuer: String.t(),
          tenant_id: String.t(),
          client_id: String.t(),
          agent: LiteLLM.LiteLLMAgentsTable.t() | nil
        }

  schema "LiteLLM_RetiredAgentIdentity" do
    field(:provider, :string)
    field(:issuer, :string)
    field(:tenant_id, :string)
    field(:client_id, :string)
    belongs_to(:agent, LiteLLM.LiteLLMAgentsTable, foreign_key: :agent_id)
  end
end