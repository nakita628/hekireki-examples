defmodule Langfuse.SkillBlob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          sha256_hash: String.t(),
          content_type: String.t(),
          content_length: integer(),
          content: String.t(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          files: [Langfuse.SkillFile.t()]
        }

  schema "skill_blobs" do
    field(:id, :string, primary_key: true)
    field(:sha256_hash, :string, source: :sha_256_hash)
    field(:content_type, :string)
    field(:content_length, :integer)
    field(:content, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:files, Langfuse.SkillFile, foreign_key: :blob_id, references: :id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end