defmodule Langfuse.BatchAction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          action_type: String.t(),
          table_name: String.t(),
          status: String.t(),
          finished_at: DateTime.t() | nil,
          query: map(),
          config: map() | nil,
          total_count: integer() | nil,
          processed_count: integer() | nil,
          failed_count: integer() | nil,
          log: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "batch_actions" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string)
    field(:action_type, :string)
    field(:table_name, :string)
    field(:status, :string)
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:query, :map)
    field(:config, :map)
    field(:total_count, :integer)
    field(:processed_count, :integer)
    field(:failed_count, :integer)
    field(:log, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end