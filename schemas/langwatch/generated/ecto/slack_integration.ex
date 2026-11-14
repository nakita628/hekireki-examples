defmodule LangWatch.SlackIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          kind: atom(),
          scope_type: atom(),
          scope_id: String.t(),
          organization_id: String.t(),
          bot_token_encrypted: String.t() | nil,
          webhook_url_encrypted: String.t() | nil,
          secret_fingerprint: String.t(),
          secret_hint: String.t(),
          slack_team_id: String.t() | nil,
          slack_team_name: String.t() | nil,
          created_by_id: String.t(),
          updated_by_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SlackIntegration" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:kind, Ecto.Enum, values: [:BOT, :INCOMING_WEBHOOK])
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:organization_id, :string, source: :organizationId)
    field(:bot_token_encrypted, :string, source: :botTokenEncrypted)
    field(:webhook_url_encrypted, :string, source: :webhookUrlEncrypted)
    field(:secret_fingerprint, :string, source: :secretFingerprint)
    field(:secret_hint, :string, source: :secretHint)
    field(:slack_team_id, :string, source: :slackTeamId)
    field(:slack_team_name, :string, source: :slackTeamName)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end