defmodule LangWatch.AiToolEntry do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope: String.t(),
          scope_id: String.t(),
          type: String.t(),
          display_name: String.t(),
          slug: String.t(),
          icon_key: String.t() | nil,
          icon_asset: String.t() | nil,
          order: integer(),
          enabled: boolean(),
          config: map(),
          archived_at: DateTime.t() | nil,
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          teams: [LangWatch.AiToolEntryTeam.t()],
          departments: [LangWatch.AiToolEntryDepartment.t()]
        }

  schema "AiToolEntry" do
    field(:id, :string, primary_key: true)
    field(:scope, :string)
    field(:scope_id, :string, source: :scopeId)
    field(:type, :string)
    field(:display_name, :string, source: :displayName)
    field(:slug, :string)
    field(:icon_key, :string, source: :iconKey)
    field(:icon_asset, :string, source: :iconAsset)
    field(:order, :integer, default: 0)
    field(:enabled, :boolean, default: true)
    field(:config, :map, default: %{})
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:teams, LangWatch.AiToolEntryTeam, foreign_key: :entry_id, references: :id)
    has_many(:departments, LangWatch.AiToolEntryDepartment, foreign_key: :entry_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end