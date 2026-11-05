defmodule OpenPanel.GscConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          site_url: String.t(),
          access_token: String.t(),
          refresh_token: String.t(),
          access_token_expires_at: DateTime.t() | nil,
          last_synced_at: DateTime.t() | nil,
          last_sync_status: String.t() | nil,
          last_sync_error: String.t() | nil,
          backfill_status: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil
        }

  schema "gsc_connections" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:site_url, :string, default: "", source: :siteUrl)
    field(:access_token, :string, source: :accessToken)
    field(:refresh_token, :string, source: :refreshToken)
    field(:access_token_expires_at, OpenPanel.PrismaDateTime, source: :accessTokenExpiresAt)
    field(:last_synced_at, OpenPanel.PrismaDateTime, source: :lastSyncedAt)
    field(:last_sync_status, :string, source: :lastSyncStatus)
    field(:last_sync_error, :string, source: :lastSyncError)
    field(:backfill_status, :string, source: :backfillStatus)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end