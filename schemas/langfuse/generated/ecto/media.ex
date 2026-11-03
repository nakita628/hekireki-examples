defmodule Langfuse.Media do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          id: String.t(),
          sha256_hash: String.t(),
          uploaded_at: DateTime.t() | nil,
          upload_http_status: integer() | nil,
          upload_http_error: String.t() | nil,
          bucket_path: String.t(),
          bucket_name: String.t(),
          content_type: String.t(),
          content_length: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "media" do
    field(:id, :string, primary_key: true)
    field(:sha256_hash, :string, source: :sha_256_hash)
    field(:uploaded_at, Langfuse.PrismaDateTime)
    field(:upload_http_status, :integer)
    field(:upload_http_error, :string)
    field(:bucket_path, :string)
    field(:bucket_name, :string)
    field(:content_type, :string)
    field(:content_length, :integer)
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end