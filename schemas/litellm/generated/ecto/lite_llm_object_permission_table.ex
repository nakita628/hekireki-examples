defmodule LiteLLM.LiteLLMObjectPermissionTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :object_permission_id}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          mcp_servers: [String.t()],
          mcp_access_groups: [String.t()],
          mcp_tool_permissions: map() | nil,
          vector_stores: [String.t()],
          agents: [String.t()],
          agent_access_groups: [String.t()],
          models: [String.t()],
          blocked_tools: [String.t()],
          mcp_toolsets: [String.t()],
          search_tools: [String.t()],
          mcp_tool_search_enabled: boolean() | nil,
          skills: [String.t()],
          teams: [LiteLLM.LiteLLMTeamTable.t()],
          projects: [LiteLLM.LiteLLMProjectTable.t()],
          verification_tokens: [LiteLLM.LiteLLMVerificationToken.t()],
          organizations: [LiteLLM.LiteLLMOrganizationTable.t()],
          users: [LiteLLM.LiteLLMUserTable.t()],
          end_users: [LiteLLM.LiteLLMEndUserTable.t()],
          agents_table: [LiteLLM.LiteLLMAgentsTable.t()]
        }

  schema "LiteLLM_ObjectPermissionTable" do
    field(:mcp_servers, {:array, :string})
    field(:mcp_access_groups, {:array, :string})
    field(:mcp_tool_permissions, :map)
    field(:vector_stores, {:array, :string})
    field(:agents, {:array, :string})
    field(:agent_access_groups, {:array, :string})
    field(:models, {:array, :string})
    field(:blocked_tools, {:array, :string})
    field(:mcp_toolsets, {:array, :string})
    field(:search_tools, {:array, :string})
    field(:mcp_tool_search_enabled, :boolean)
    field(:skills, {:array, :string})
    has_many(:teams, LiteLLM.LiteLLMTeamTable, foreign_key: :object_permission_id)
    has_many(:projects, LiteLLM.LiteLLMProjectTable, foreign_key: :object_permission_id)
    has_many(:verification_tokens, LiteLLM.LiteLLMVerificationToken, foreign_key: :object_permission_id)
    has_many(:organizations, LiteLLM.LiteLLMOrganizationTable, foreign_key: :object_permission_id)
    has_many(:users, LiteLLM.LiteLLMUserTable, foreign_key: :object_permission_id)
    has_many(:end_users, LiteLLM.LiteLLMEndUserTable, foreign_key: :object_permission_id)
    has_many(:agents_table, LiteLLM.LiteLLMAgentsTable, foreign_key: :object_permission_id)
  end
end