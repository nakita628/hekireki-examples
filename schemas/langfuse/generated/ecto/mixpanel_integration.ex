defmodule Langfuse.MixpanelIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          encrypted_mixpanel_project_token: String.t(),
          mixpanel_region: String.t(),
          last_sync_at: DateTime.t() | nil,
          enabled: boolean(),
          export_source: atom(),
          backfill: boolean(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "mixpanel_integrations" do
    field(:encrypted_mixpanel_project_token, :string)
    field(:mixpanel_region, :string)
    field(:last_sync_at, Langfuse.PrismaDateTime)
    field(:enabled, :boolean)
    field(:export_source, Ecto.Enum, values: [:TRACES_OBSERVATIONS, :TRACES_OBSERVATIONS_EVENTS, :EVENTS], default: :TRACES_OBSERVATIONS)
    field(:backfill, :boolean, default: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end