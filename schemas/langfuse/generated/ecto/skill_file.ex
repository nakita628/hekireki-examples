defmodule Langfuse.SkillFile do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          path: String.t(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          skill: Langfuse.Skill.t() | nil,
          blob: Langfuse.SkillBlob.t() | nil
        }

  schema "skill_files" do
    field(:id, :string, primary_key: true)
    field(:path, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:skill, Langfuse.Skill, foreign_key: :skill_id, type: :string)
    belongs_to(:blob, Langfuse.SkillBlob, foreign_key: :blob_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end