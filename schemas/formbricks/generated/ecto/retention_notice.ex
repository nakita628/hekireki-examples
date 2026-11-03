defmodule Formbricks.RetentionNotice do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          entity: atom(),
          sent_at: DateTime.t(),
          delivered_at: DateTime.t() | nil,
          email_sent: boolean(),
          claim_token: String.t() | nil,
          clock_at: DateTime.t() | nil,
          organization: Formbricks.Organization.t() | nil,
          survey: Formbricks.Survey.t() | nil,
          user: Formbricks.User.t() | nil
        }

  schema "RetentionNotice" do
    field(:id, :string, primary_key: true)
    field(:entity, Ecto.Enum, values: [:responses, :surveys, :members])
    field(:sent_at, Formbricks.PrismaDateTime, autogenerate: true, source: :sentAt)
    field(:delivered_at, Formbricks.PrismaDateTime, source: :deliveredAt)
    field(:email_sent, :boolean, default: false, source: :emailSent)
    field(:claim_token, :string, source: :claimToken)
    field(:clock_at, Formbricks.PrismaDateTime, source: :clockAt)
    field(:organization_id, :string, source: :organizationId)
    field(:survey_id, :string, source: :surveyId)
    field(:user_id, :string, source: :userId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end