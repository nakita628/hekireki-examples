defmodule LangWatch.EmailSuppression do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          trigger_id: String.t() | nil,
          reason: String.t(),
          inserted_at: DateTime.t(),
          project: LangWatch.Project.t() | nil
        }

  schema "EmailSuppression" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:trigger_id, :string, source: :triggerId)
    field(:reason, :string, default: "unsubscribe")
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end