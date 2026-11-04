defmodule Trigger.PlatformNotification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          title: String.t(),
          payload: map(),
          surface: atom(),
          scope: atom(),
          starts_at: DateTime.t(),
          ends_at: DateTime.t(),
          cli_max_days_after_first_seen: integer() | nil,
          cli_max_show_count: integer() | nil,
          cli_show_every: integer() | nil,
          priority: integer(),
          is_draft: boolean(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Trigger.User.t() | nil,
          organization: Trigger.Organization.t() | nil,
          project: Trigger.Project.t() | nil,
          interactions: [Trigger.PlatformNotificationInteraction.t()]
        }

  schema "PlatformNotification" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:title, :string)
    field(:payload, :map)
    field(:surface, Ecto.Enum, values: [:WEBAPP, :CLI])
    field(:scope, Ecto.Enum, values: [:USER, :PROJECT, :ORGANIZATION, :GLOBAL])
    field(:starts_at, Trigger.PrismaDateTime, source: :startsAt)
    field(:ends_at, Trigger.PrismaDateTime, source: :endsAt)
    field(:cli_max_days_after_first_seen, :integer, source: :cliMaxDaysAfterFirstSeen)
    field(:cli_max_show_count, :integer, source: :cliMaxShowCount)
    field(:cli_show_every, :integer, source: :cliShowEvery)
    field(:priority, :integer, default: 0)
    field(:is_draft, :boolean, default: false, source: :isDraft)
    field(:archived_at, Trigger.PrismaDateTime, source: :archivedAt)
    field(:user_id, :string, source: :userId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:interactions, Trigger.PlatformNotificationInteraction, foreign_key: :notification_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end