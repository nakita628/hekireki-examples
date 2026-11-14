defmodule LangWatch.DashboardFavourite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          dashboard_id: String.t() | nil,
          template_id: String.t() | nil,
          project_id: String.t(),
          position: integer(),
          inserted_at: DateTime.t()
        }

  schema "DashboardFavourite" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:dashboard_id, :string, source: :dashboardId)
    field(:template_id, :string, source: :templateId)
    field(:project_id, :string, source: :projectId)
    field(:position, :integer)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end