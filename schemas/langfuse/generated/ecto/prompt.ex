defmodule Langfuse.Prompt do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          created_by: String.t(),
          prompt: map(),
          name: String.t(),
          version: integer(),
          type: String.t(),
          is_active: boolean() | nil,
          config: map(),
          tags: [String.t()],
          labels: [String.t()],
          commit_message: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          prompt_dependency: [Langfuse.PromptDependency.t()]
        }

  schema "prompts" do
    field(:id, :string, primary_key: true)
    field(:created_by, :string)
    field(:prompt, :map)
    field(:name, :string)
    field(:version, :integer)
    field(:type, :string, default: "text")
    field(:is_active, :boolean)
    field(:config, :map, default: %{})
    field(:tags, {:array, :string})
    field(:labels, {:array, :string})
    field(:commit_message, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:prompt_dependency, Langfuse.PromptDependency, foreign_key: :parent_id)
    timestamps(inserted_at_source: :created_at)
  end
end