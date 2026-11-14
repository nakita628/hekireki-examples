defmodule LangWatch.ProjectActiveDay do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          day: String.t(),
          inserted_at: DateTime.t()
        }

  schema "ProjectActiveDay" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:day, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end