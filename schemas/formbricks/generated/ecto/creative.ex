defmodule Formbricks.Creative do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: atom(),
          status: atom(),
          published_at: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          content: map(),
          styling: map(),
          survey_id: String.t() | nil,
          link_url: String.t() | nil,
          show_on: atom(),
          show_conditions: map() | nil,
          display_percentage: float() | nil,
          start_at: DateTime.t() | nil,
          end_at: DateTime.t() | nil,
          auto_select_language: boolean() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          creator: Formbricks.User.t() | nil,
          triggers: [Formbricks.CreativeTrigger.t()],
          languages: [Formbricks.CreativeLanguage.t()],
          daily_stats: [Formbricks.CreativeDailyStat.t()],
          daily_page_stats: [Formbricks.CreativeDailyPageStat.t()]
        }

  schema "Creative" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:feedbackButton, :banner])
    field(:status, Ecto.Enum, values: [:draft, :inProgress, :paused, :completed], default: :draft)
    field(:published_at, Formbricks.PrismaDateTime, source: :publishedAt)
    field(:archived_at, Formbricks.PrismaDateTime, source: :archivedAt)
    field(:content, :map)
    field(:styling, :map)
    field(:survey_id, :string, source: :surveyId)
    field(:link_url, :string, source: :linkUrl)
    field(:show_on, Ecto.Enum, values: [:pageView, :action], default: :pageView, source: :showOn)
    field(:show_conditions, :map, source: :showConditions)
    field(:display_percentage, :float, source: :displayPercentage)
    field(:start_at, Formbricks.PrismaDateTime, source: :startAt)
    field(:end_at, Formbricks.PrismaDateTime, source: :endAt)
    field(:auto_select_language, :boolean, source: :autoSelectLanguage)
    field(:workspace_id, :string, source: :workspaceId)
    field(:created_by, :string, source: :createdBy)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :created_by, define_field: false, type: :string)
    has_many(:triggers, Formbricks.CreativeTrigger, foreign_key: :creative_id)
    has_many(:languages, Formbricks.CreativeLanguage, foreign_key: :creative_id)
    has_many(:daily_stats, Formbricks.CreativeDailyStat, foreign_key: :creative_id)
    has_many(:daily_page_stats, Formbricks.CreativeDailyPageStat, foreign_key: :creative_id)
    timestamps(inserted_at_source: :created_at)
  end
end