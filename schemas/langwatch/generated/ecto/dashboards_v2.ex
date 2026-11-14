defmodule LangWatch.DashboardsV2 do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          created_by_id: String.t() | nil,
          scope: atom(),
          organization_id: String.t() | nil,
          excluded_origins: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DashboardsV2" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:name, :string)
    field(:description, :string)
    field(:created_by_id, :string, source: :createdById)
    field(:scope, Ecto.Enum, values: [:PRIVATE, :PROJECT, :ORGANIZATION], default: :PROJECT)
    field(:organization_id, :string, source: :organizationId)
    field(:excluded_origins, :map, source: :excludedOrigins)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end