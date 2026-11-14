defmodule LangWatch.GrantUsage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          grant_id: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          view_count: integer(),
          last_viewed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "GrantUsage" do
    field(:grant_id, :string, primary_key: true, source: :grantId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:view_count, :integer, default: 0, source: :viewCount)
    field(:last_viewed_at, LangWatch.PrismaDateTime, source: :lastViewedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end