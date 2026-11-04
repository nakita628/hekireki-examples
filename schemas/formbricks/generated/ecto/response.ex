defmodule Formbricks.Response do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          finished: boolean(),
          ending_id: String.t() | nil,
          data: map(),
          variables: map(),
          ttc: map(),
          meta: map(),
          ingest_flags: map() | nil,
          outcome: atom() | nil,
          contact_attributes: map() | nil,
          single_use_id: String.t() | nil,
          language: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil,
          contact: Formbricks.Contact.t() | nil,
          display: Formbricks.Display.t() | nil,
          tags: [Formbricks.TagsOnResponses.t()],
          quota_links: [Formbricks.ResponseQuotaLink.t()],
          workflow_runs: [Formbricks.WorkflowRun.t()]
        }

  schema "Response" do
    field(:id, :string, primary_key: true)
    field(:finished, :boolean, default: false)
    field(:ending_id, :string, source: :endingId)
    field(:data, :map, default: %{})
    field(:variables, :map, default: %{})
    field(:ttc, :map, default: %{})
    field(:meta, :map, default: %{})
    field(:ingest_flags, :map)
    field(:outcome, Ecto.Enum, values: [:lowQuality])
    field(:contact_attributes, :map, source: :contactAttributes)
    field(:single_use_id, :string, source: :singleUseId)
    field(:language, :string)
    field(:survey_id, :string, source: :surveyId)
    field(:contact_id, :string, source: :contactId)
    field(:display_id, :string, source: :displayId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    belongs_to(:contact, Formbricks.Contact, foreign_key: :contact_id, define_field: false, type: :string)
    belongs_to(:display, Formbricks.Display, foreign_key: :display_id, define_field: false, type: :string)
    has_many(:tags, Formbricks.TagsOnResponses, foreign_key: :response_id)
    has_many(:quota_links, Formbricks.ResponseQuotaLink, foreign_key: :response_id)
    has_many(:workflow_runs, Formbricks.WorkflowRun, foreign_key: :response_id)
    timestamps(inserted_at_source: :created_at)
  end
end