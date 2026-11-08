defmodule Langfuse.BlobStorageIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          type: atom(),
          bucket_name: String.t(),
          prefix: String.t(),
          auth_type: atom(),
          access_key_id: String.t() | nil,
          secret_access_key: String.t() | nil,
          role_arn: String.t() | nil,
          external_id: String.t() | nil,
          region: String.t(),
          endpoint: String.t() | nil,
          force_path_style: boolean(),
          next_sync_at: DateTime.t() | nil,
          last_sync_at: DateTime.t() | nil,
          enabled: boolean(),
          export_frequency: String.t(),
          file_type: atom(),
          export_mode: atom(),
          export_start_date: DateTime.t() | nil,
          export_source: atom(),
          export_field_groups: [String.t()],
          compressed: boolean(),
          export_tuning: map() | nil,
          run_started_at: DateTime.t() | nil,
          backfill: boolean(),
          last_error: String.t() | nil,
          last_error_at: DateTime.t() | nil,
          last_failure_notification_sent_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "blob_storage_integrations" do
    field(:type, Ecto.Enum, values: [:S3, :S3_COMPATIBLE, :AZURE_BLOB_STORAGE, :GOOGLE_CLOUD_STORAGE])
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
    field(:next_sync_at, Langfuse.PrismaDateTime)
    field(:last_sync_at, Langfuse.PrismaDateTime)
    field(:enabled, :boolean)
    field(:export_frequency, :string)
    field(:file_type, Ecto.Enum, values: [:JSON, :CSV, :JSONL, :PARQUET], default: :CSV)
    field(:export_mode, Ecto.Enum, values: [:FULL_HISTORY, :FROM_TODAY, :FROM_CUSTOM_DATE], default: :FULL_HISTORY)
    field(:export_start_date, Langfuse.PrismaDateTime)
    field(:export_source, Ecto.Enum, values: [:TRACES_OBSERVATIONS, :TRACES_OBSERVATIONS_EVENTS, :EVENTS], default: :TRACES_OBSERVATIONS)
    field(:export_field_groups, {:array, :string})
    field(:compressed, :boolean, default: true)
    field(:export_tuning, :map)
    field(:run_started_at, Langfuse.PrismaDateTime)
    field(:backfill, :boolean, default: true)
    field(:last_error, :string)
    field(:last_error_at, Langfuse.PrismaDateTime)
    field(:last_failure_notification_sent_at, Langfuse.PrismaDateTime)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end