defmodule Langfuse.BatchExport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          finished_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          name: String.t(),
          status: String.t(),
          query: map(),
          format: String.t(),
          url: String.t() | nil,
          log: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "batch_exports" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string)
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:expires_at, Langfuse.PrismaDateTime)
    field(:name, :string)
    field(:status, :string)
    field(:query, :map)
    field(:format, :string)
    field(:url, :string)
    field(:log, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end