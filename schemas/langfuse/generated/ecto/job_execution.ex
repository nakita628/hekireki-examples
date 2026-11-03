defmodule Langfuse.JobExecution do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          job_configuration_id: String.t(),
          job_template_id: String.t() | nil,
          status: atom(),
          start_time: DateTime.t() | nil,
          end_time: DateTime.t() | nil,
          error: String.t() | nil,
          job_input_trace_id: String.t() | nil,
          job_input_trace_timestamp: DateTime.t() | nil,
          job_input_observation_id: String.t() | nil,
          job_input_dataset_item_id: String.t() | nil,
          job_input_dataset_item_valid_from: DateTime.t() | nil,
          job_output_score_id: String.t() | nil,
          execution_trace_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "job_executions" do
    field(:id, :string, primary_key: true)
    field(:job_configuration_id, :string)
    field(:job_template_id, :string)
    field(:status, Ecto.Enum, values: [:COMPLETED, :ERROR, :PENDING, :CANCELLED, :DELAYED])
    field(:start_time, Langfuse.PrismaDateTime)
    field(:end_time, Langfuse.PrismaDateTime)
    field(:error, :string)
    field(:job_input_trace_id, :string)
    field(:job_input_trace_timestamp, Langfuse.PrismaDateTime)
    field(:job_input_observation_id, :string)
    field(:job_input_dataset_item_id, :string)
    field(:job_input_dataset_item_valid_from, Langfuse.PrismaDateTime)
    field(:job_output_score_id, :string)
    field(:execution_trace_id, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end