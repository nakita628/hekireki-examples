defmodule Langfuse.ExternalMediaStorageIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          type: atom(),
          bucket_name: String.t(),
          prefix: String.t() | nil,
          auth_type: atom(),
          access_key_id: String.t() | nil,
          secret_access_key: String.t() | nil,
          role_arn: String.t() | nil,
          external_id: String.t() | nil,
          region: String.t(),
          endpoint: String.t() | nil,
          force_path_style: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "external_media_storage_integrations" do
    field(:type, Ecto.Enum, values: [:S3, :S3_COMPATIBLE])
    field(:bucket_name, :string)
    field(:prefix, :string)
    field(:auth_type, Ecto.Enum, values: [:ACCESS_KEYS, :ROLE_ARN], default: :ACCESS_KEYS)
    field(:access_key_id, :string)
    field(:secret_access_key, :string)
    field(:role_arn, :string)
    field(:external_id, :string)
    field(:region, :string)
    field(:endpoint, :string)
    field(:force_path_style, :boolean)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end