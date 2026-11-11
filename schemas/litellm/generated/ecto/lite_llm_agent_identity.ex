defmodule LiteLLM.LiteLLMAgentIdentity do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          active: boolean(),
          provider: String.t(),
          issuer: String.t(),
          tenant_id: String.t(),
          client_id: String.t(),
          service_principal_id: String.t() | nil,
          required_roles: [String.t()],
          required_scopes: [String.t()],
          revision: String.t(),
          last_authenticated_at: DateTime.t() | nil,
          agent: LiteLLM.LiteLLMAgentsTable.t() | nil
        }

  schema "LiteLLM_AgentIdentity" do
    field(:active, :boolean, default: true)
    field(:provider, :string)
    field(:issuer, :string)
    field(:tenant_id, :string)
    field(:client_id, :string)
    field(:service_principal_id, :string)
    field(:required_roles, {:array, :string})
    field(:required_scopes, {:array, :string})
    field(:revision, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:last_authenticated_at, LiteLLM.PrismaDateTime)
    belongs_to(:agent, LiteLLM.LiteLLMAgentsTable, foreign_key: :agent_id, type: :binary_id)
  end
end