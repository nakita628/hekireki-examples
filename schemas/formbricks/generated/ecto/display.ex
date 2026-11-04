defmodule Formbricks.Display do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          creative_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil,
          contact: Formbricks.Contact.t() | nil,
          response: Formbricks.Response.t() | nil
        }

  schema "Display" do
    field(:id, :string, primary_key: true)
    field(:creative_id, :string, source: :creativeId)
    field(:survey_id, :string, source: :surveyId)
    field(:contact_id, :string, source: :contactId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    belongs_to(:contact, Formbricks.Contact, foreign_key: :contact_id, define_field: false, type: :string)
    has_one(:response, Formbricks.Response, foreign_key: :display_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end