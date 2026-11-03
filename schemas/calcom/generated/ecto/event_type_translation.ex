defmodule Calcom.EventTypeTranslation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          uid: String.t(),
          field: atom(),
          source_locale: String.t(),
          target_locale: String.t(),
          translated_text: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          event_type: Calcom.EventType.t() | nil,
          creator: Calcom.User.t() | nil,
          updater: Calcom.User.t() | nil
        }

  schema "EventTypeTranslation" do
    field(:uid, :string, primary_key: true)
    field(:field, Ecto.Enum, values: [:DESCRIPTION, :TITLE])
    field(:source_locale, :string, source: :sourceLocale)
    field(:target_locale, :string, source: :targetLocale)
    field(:translated_text, :string, source: :translatedText)
    field(:event_type_id, :id, source: :eventTypeId)
    field(:created_by, :id, source: :createdBy)
    field(:updated_by, :id, source: :updatedBy)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:creator, Calcom.User, foreign_key: :created_by, define_field: false)
    belongs_to(:updater, Calcom.User, foreign_key: :updated_by, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end