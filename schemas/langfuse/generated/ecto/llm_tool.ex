defmodule Langfuse.LlmTool do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t(),
          parameters: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "llm_tools" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:parameters, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end