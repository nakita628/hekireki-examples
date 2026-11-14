defmodule LangWatch.IngestionTemplate do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          source_type: String.t(),
          display_name: String.t(),
          description: String.t() | nil,
          icon_asset: String.t() | nil,
          credential_schema: String.t() | nil,
          ottl_rules: String.t(),
          platform_published: boolean(),
          enabled: boolean(),
          archived_at: DateTime.t() | nil,
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "IngestionTemplate" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:source_type, :string, source: :sourceType)
    field(:display_name, :string, source: :displayName)
    field(:description, :string)
    field(:icon_asset, :string, source: :iconAsset)
    field(:credential_schema, :string, source: :credentialSchema)
    field(:ottl_rules, :string, default: "", source: :ottlRules)
    field(:platform_published, :boolean, default: false, source: :platformPublished)
    field(:enabled, :boolean, default: true)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end