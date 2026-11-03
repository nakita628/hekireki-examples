defmodule Calcom.CalVideoSettings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          disable_recording_for_organizer: boolean(),
          disable_recording_for_guests: boolean(),
          enable_automatic_transcription: boolean(),
          enable_automatic_recording_for_organizer: boolean(),
          redirect_url_on_exit: String.t() | nil,
          disable_transcription_for_guests: boolean(),
          disable_transcription_for_organizer: boolean(),
          require_email_for_guests: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          event_type: Calcom.EventType.t() | nil
        }

  schema "CalVideoSettings" do
    field(:disable_recording_for_organizer, :boolean, default: false, source: :disableRecordingForOrganizer)
    field(:disable_recording_for_guests, :boolean, default: false, source: :disableRecordingForGuests)
    field(:enable_automatic_transcription, :boolean, default: false, source: :enableAutomaticTranscription)
    field(:enable_automatic_recording_for_organizer, :boolean, default: false, source: :enableAutomaticRecordingForOrganizer)
    field(:redirect_url_on_exit, :string, source: :redirectUrlOnExit)
    field(:disable_transcription_for_guests, :boolean, default: false, source: :disableTranscriptionForGuests)
    field(:disable_transcription_for_organizer, :boolean, default: false, source: :disableTranscriptionForOrganizer)
    field(:require_email_for_guests, :boolean, default: false, source: :requireEmailForGuests)
    field(:event_type_id, :id, source: :eventTypeId)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end