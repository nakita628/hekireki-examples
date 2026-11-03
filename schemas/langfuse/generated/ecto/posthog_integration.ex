defmodule Langfuse.PosthogIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          encrypted_posthog_api_key: String.t(),
          posthog_host_name: String.t(),
          last_sync_at: DateTime.t() | nil,
          enabled: boolean(),
          export_source: atom(),
          backfill: boolean(),
          last_error: String.t() | nil,
          last_error_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "posthog_integrations" do
    field(:encrypted_posthog_api_key, :string)
    field(:posthog_host_name, :string)
    field(:last_sync_at, Langfuse.PrismaDateTime)
    field(:enabled, :boolean)
    field(:export_source, Ecto.Enum, values: [:TRACES_OBSERVATIONS, :TRACES_OBSERVATIONS_EVENTS, :EVENTS], default: :TRACES_OBSERVATIONS)
    field(:backfill, :boolean, default: true)
    field(:last_error, :string)
    field(:last_error_at, Langfuse.PrismaDateTime)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end