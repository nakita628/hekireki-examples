defmodule LiteLLM.LiteLLMAccessGroupTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :access_group_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          access_group_name: String.t(),
          description: String.t() | nil,
          access_model_names: [String.t()],
          access_mcp_server_ids: [String.t()],
          mcp_tool_permissions: map() | nil,
          access_agent_ids: [String.t()],
          access_skill_names: [String.t()],
          assigned_team_ids: [String.t()],
          assigned_key_ids: [String.t()],
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_AccessGroupTable" do
    field(:access_group_name, :string)
    field(:description, :string)
    field(:access_model_names, {:array, :string})
    field(:access_mcp_server_ids, {:array, :string})
    field(:mcp_tool_permissions, :map)
    field(:access_agent_ids, {:array, :string})
    field(:access_skill_names, {:array, :string})
    field(:assigned_team_ids, {:array, :string})
    field(:assigned_key_ids, {:array, :string})
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end