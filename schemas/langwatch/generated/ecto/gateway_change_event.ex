defmodule LangWatch.GatewayChangeEvent do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true, source: :revision}
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          kind: atom(),
          virtual_key_id: String.t() | nil,
          budget_id: String.t() | nil,
          model_provider_id: String.t() | nil,
          payload: map() | nil,
          inserted_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          project: LangWatch.Project.t() | nil
        }

  schema "GatewayChangeEvent" do
    field(:kind, Ecto.Enum, values: [:VK_CREATED, :VK_CONFIG_UPDATED, :VK_REVOKED, :VK_ROTATED, :VK_DISABLED, :VK_ENABLED, :BUDGET_CREATED, :BUDGET_UPDATED, :BUDGET_DELETED, :MODEL_PROVIDER_UPDATED, :CACHE_RULE_CREATED, :CACHE_RULE_UPDATED, :CACHE_RULE_DELETED, :ROUTING_POLICY_UPDATED, :ROUTING_POLICY_DELETED])
    field(:virtual_key_id, :string, source: :virtualKeyId)
    field(:budget_id, :string, source: :budgetId)
    field(:model_provider_id, :string, source: :modelProviderId)
    field(:payload, :map)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end