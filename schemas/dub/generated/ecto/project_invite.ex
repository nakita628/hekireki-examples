defmodule Dub.ProjectInvite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          email: String.t(),
          expires: DateTime.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          project: Dub.Project.t() | nil
        }

  schema "ProjectInvite" do
    field(:email, :string, primary_key: true)
    field(:expires, Dub.PrismaDateTime)
    field(:role, Ecto.Enum, values: [:owner, :member, :viewer, :billing], default: :member)
    field(:project_id, :string, primary_key: true, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end