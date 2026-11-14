defmodule LangWatch.AnomalyRule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope: String.t(),
          scope_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          severity: String.t(),
          rule_type: String.t(),
          threshold_config: map(),
          destination_config: map(),
          status: String.t(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          created_by: LangWatch.User.t() | nil,
          alerts: [LangWatch.AnomalyAlert.t()]
        }

  schema "AnomalyRule" do
    field(:id, :string, primary_key: true)
    field(:scope, :string)
    field(:scope_id, :string, source: :scopeId)
    field(:name, :string)
    field(:description, :string)
    field(:severity, :string)
    field(:rule_type, :string, source: :ruleType)
    field(:threshold_config, :map, default: %{}, source: :thresholdConfig)
    field(:destination_config, :map, default: %{}, source: :destinationConfig)
    field(:status, :string, default: "active")
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:created_by_id, :string, source: :createdById)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    has_many(:alerts, LangWatch.AnomalyAlert, foreign_key: :rule_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end