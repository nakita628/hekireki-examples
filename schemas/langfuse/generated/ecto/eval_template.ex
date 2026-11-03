defmodule Langfuse.EvalTemplate do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          version: integer(),
          prompt: String.t() | nil,
          type: atom(),
          partner: String.t() | nil,
          model: String.t() | nil,
          provider: String.t() | nil,
          model_params: map() | nil,
          vars: [String.t()],
          output_definition: map() | nil,
          source_code: String.t() | nil,
          source_code_language: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          job_configuration: [Langfuse.JobConfiguration.t()]
        }

  schema "eval_templates" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:version, :integer)
    field(:prompt, :string)
    field(:type, Ecto.Enum, values: [:LLM_AS_JUDGE, :CODE, :DECISION_MODEL, :FACET], default: :LLM_AS_JUDGE)
    field(:partner, :string)
    field(:model, :string)
    field(:provider, :string)
    field(:model_params, :map)
    field(:vars, {:array, :string})
    field(:output_definition, :map, source: :output_schema)
    field(:source_code, :string)
    field(:source_code_language, Ecto.Enum, values: [:PYTHON, :TYPESCRIPT])
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:job_configuration, Langfuse.JobConfiguration, foreign_key: :eval_template_id)
    timestamps(inserted_at_source: :created_at)
  end
end