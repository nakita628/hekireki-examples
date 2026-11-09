defmodule Jitsu.ReprocessingWorkers do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          job_id: String.t(),
          worker_index: integer(),
          status: String.t(),
          started_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          current_file: String.t() | nil,
          current_line: integer() | nil,
          current_ts: String.t() | nil,
          assigned_files: integer() | nil,
          processed_files: integer() | nil,
          total_lines: integer() | nil,
          success_count: integer() | nil,
          error_count: integer() | nil,
          skipped_count: integer() | nil,
          total_bytes: integer() | nil,
          processed_bytes: integer() | nil,
          error: String.t() | nil,
          job: Jitsu.ReprocessingJobs.t() | nil
        }

  schema "reprocessing_workers" do
    field(:worker_index, :integer, primary_key: true)
    field(:status, :string)
    field(:started_at, Jitsu.PrismaDateTime)
    field(:updated_at, Jitsu.PrismaDateTime, autogenerate: true)
    field(:completed_at, Jitsu.PrismaDateTime)
    field(:current_file, :string)
    field(:current_line, :integer, default: 0)
    field(:current_ts, :string)
    field(:assigned_files, :integer, default: 0)
    field(:processed_files, :integer, default: 0)
    field(:total_lines, :integer, default: 0)
    field(:success_count, :integer, default: 0)
    field(:error_count, :integer, default: 0)
    field(:skipped_count, :integer, default: 0)
    field(:total_bytes, :integer, default: 0)
    field(:processed_bytes, :integer, default: 0)
    field(:error, :string)
    field(:job_id, :string, primary_key: true)
    belongs_to(:job, Jitsu.ReprocessingJobs, foreign_key: :job_id, define_field: false, type: :string)
  end
end