defmodule Langfuse.TraceMedia do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          media_id: String.t(),
          trace_id: String.t(),
          field: String.t(),
          origin: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "trace_media" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string)
    field(:media_id, :string)
    field(:trace_id, :string)
    field(:field, :string)
    field(:origin, Ecto.Enum, values: [:UNKNOWN, :CLIENT_UPLOAD, :INGESTION_MEDIA_EXTRACTION, :INGESTION_FIELD_OVERFLOW], default: :UNKNOWN)
    timestamps(inserted_at_source: :created_at)
  end
end