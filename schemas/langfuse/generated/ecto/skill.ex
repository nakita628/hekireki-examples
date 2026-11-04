defmodule Langfuse.Skill do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          created_by: String.t(),
          name: String.t(),
          description: String.t(),
          frontmatter: map(),
          version: integer(),
          tags: [String.t()],
          labels: [String.t()],
          commit_message: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          files: [Langfuse.SkillFile.t()]
        }

  schema "skills" do
    field(:id, :string, primary_key: true)
    field(:created_by, :string)
    field(:name, :string)
    field(:description, :string)
    field(:frontmatter, :map, default: %{})
    field(:version, :integer)
    field(:tags, {:array, :string})
    field(:labels, {:array, :string})
    field(:commit_message, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:files, Langfuse.SkillFile, foreign_key: :skill_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end