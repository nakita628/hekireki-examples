defmodule Langfuse.PromptDependency do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          child_name: String.t(),
          child_label: String.t() | nil,
          child_version: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          parent: Langfuse.Prompt.t() | nil
        }

  schema "prompt_dependencies" do
    field(:id, :string, primary_key: true)
    field(:child_name, :string)
    field(:child_label, :string)
    field(:child_version, :integer)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:parent, Langfuse.Prompt, foreign_key: :parent_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end