defmodule LiteLLM.LiteLLMAgentsTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :agent_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          agent_name: String.t(),
          litellm_params: map() | nil,
          agent_card_params: map(),
          static_headers: map() | nil,
          extra_headers: [String.t()],
          kill_switch: map() | nil,
          agent_access_groups: [String.t()],
          access_group_ids: [String.t()],
          spend: float(),
          identity_managed: boolean(),
          enabled: boolean(),
          execution_mode: String.t(),
          tpm_limit: integer() | nil,
          rpm_limit: integer() | nil,
          session_tpm_limit: integer() | nil,
          session_rpm_limit: integer() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          object_permission: LiteLLM.LiteLLMObjectPermissionTable.t() | nil,
          identity: LiteLLM.LiteLLMAgentIdentity.t() | nil,
          retired_identities: [LiteLLM.LiteLLMRetiredAgentIdentity.t()]
        }

  schema "LiteLLM_AgentsTable" do
    field(:agent_name, :string)
    field(:litellm_params, :map)
    field(:agent_card_params, :map)
    field(:static_headers, :map, default: %{})
    field(:extra_headers, {:array, :string})
    field(:kill_switch, :map)
    field(:agent_access_groups, {:array, :string})
    field(:access_group_ids, {:array, :string})
    field(:spend, :float, default: 0.0)
    field(:identity_managed, :boolean, default: false)
    field(:enabled, :boolean, default: true)
    field(:execution_mode, :string, default: "autonomous")
    field(:tpm_limit, :integer)
    field(:rpm_limit, :integer)
    field(:session_tpm_limit, :integer)
    field(:session_rpm_limit, :integer)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:object_permission, LiteLLM.LiteLLMObjectPermissionTable, foreign_key: :object_permission_id)
    has_one(:identity, LiteLLM.LiteLLMAgentIdentity, foreign_key: :agent_id)
    has_many(:retired_identities, LiteLLM.LiteLLMRetiredAgentIdentity, foreign_key: :agent_id)
    timestamps(inserted_at_source: :created_at)
  end
end