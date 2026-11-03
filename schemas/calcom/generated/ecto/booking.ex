defmodule Calcom.Booking do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uid: String.t(),
          idempotency_key: String.t() | nil,
          user_primary_email: String.t() | nil,
          title: String.t(),
          description: String.t() | nil,
          custom_inputs: map() | nil,
          responses: map() | nil,
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          location: String.t() | nil,
          status: atom(),
          paid: boolean(),
          cancellation_reason: String.t() | nil,
          rejection_reason: String.t() | nil,
          reassign_reason: String.t() | nil,
          dynamic_event_slug_ref: String.t() | nil,
          dynamic_group_slug_ref: String.t() | nil,
          rescheduled: boolean() | nil,
          from_reschedule: String.t() | nil,
          recurring_event_id: String.t() | nil,
          sms_reminder_number: String.t() | nil,
          scheduled_jobs: [String.t()],
          metadata: map() | nil,
          is_recorded: boolean(),
          i_cal_uid: String.t() | nil,
          i_cal_sequence: integer(),
          rating: integer() | nil,
          rating_feedback: String.t() | nil,
          no_show_host: boolean() | nil,
          one_time_password: String.t() | nil,
          cancelled_by: String.t() | nil,
          rescheduled_by: String.t() | nil,
          creation_source: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          user: Calcom.User.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          destination_calendar: Calcom.DestinationCalendar.t() | nil,
          reassign_by: Calcom.User.t() | nil,
          instant_meeting_token: Calcom.InstantMeetingToken.t() | nil,
          tracking: Calcom.Tracking.t() | nil,
          report: Calcom.BookingReport.t() | nil,
          references: [Calcom.BookingReference.t()],
          attendees: [Calcom.Attendee.t()],
          payment: [Calcom.Payment.t()],
          seats_references: [Calcom.BookingSeat.t()],
          scheduled_triggers: [Calcom.WebhookScheduledTriggers.t()],
          assignment_reason: [Calcom.AssignmentReason.t()],
          internal_note: [Calcom.BookingInternalNote.t()],
          expense_logs: [Calcom.CreditExpenseLog.t()],
          wrong_assignment_reports: [Calcom.WrongAssignmentReport.t()]
        }

  schema "Booking" do
    field(:uid, :string)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:user_primary_email, :string, source: :userPrimaryEmail)
    field(:title, :string)
    field(:description, :string)
    field(:custom_inputs, :map, source: :customInputs)
    field(:responses, :map)
    field(:start_time, Calcom.PrismaDateTime, source: :startTime)
    field(:end_time, Calcom.PrismaDateTime, source: :endTime)
    field(:location, :string)
    field(:status, Ecto.Enum, values: [CANCELLED: "cancelled", ACCEPTED: "accepted", REJECTED: "rejected", PENDING: "pending", AWAITING_HOST: "awaiting_host"], default: :ACCEPTED)
    field(:paid, :boolean, default: false)
    field(:cancellation_reason, :string, source: :cancellationReason)
    field(:rejection_reason, :string, source: :rejectionReason)
    field(:reassign_reason, :string, source: :reassignReason)
    field(:dynamic_event_slug_ref, :string, source: :dynamicEventSlugRef)
    field(:dynamic_group_slug_ref, :string, source: :dynamicGroupSlugRef)
    field(:rescheduled, :boolean)
    field(:from_reschedule, :string, source: :fromReschedule)
    field(:recurring_event_id, :string, source: :recurringEventId)
    field(:sms_reminder_number, :string, source: :smsReminderNumber)
    field(:scheduled_jobs, {:array, :string}, source: :scheduledJobs)
    field(:metadata, :map)
    field(:is_recorded, :boolean, default: false, source: :isRecorded)
    field(:i_cal_uid, :string, default: "", source: :iCalUID)
    field(:i_cal_sequence, :integer, default: 0, source: :iCalSequence)
    field(:rating, :integer)
    field(:rating_feedback, :string, source: :ratingFeedback)
    field(:no_show_host, :boolean, default: false, source: :noShowHost)
    field(:one_time_password, :string, autogenerate: {Ecto.UUID, :generate, []}, source: :oneTimePassword)
    field(:cancelled_by, :string, source: :cancelledBy)
    field(:rescheduled_by, :string, source: :rescheduledBy)
    field(:creation_source, Ecto.Enum, values: [API_V1: "api_v1", API_V2: "api_v2", WEBAPP: "webapp"], source: :creationSource)
    field(:user_id, :id, source: :userId)
    field(:event_type_id, :id, source: :eventTypeId)
    field(:destination_calendar_id, :id, source: :destinationCalendarId)
    field(:reassign_by_id, :id, source: :reassignById)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:destination_calendar, Calcom.DestinationCalendar, foreign_key: :destination_calendar_id, define_field: false)
    belongs_to(:reassign_by, Calcom.User, foreign_key: :reassign_by_id, define_field: false)
    has_one(:instant_meeting_token, Calcom.InstantMeetingToken, foreign_key: :booking_id)
    has_one(:tracking, Calcom.Tracking, foreign_key: :booking_id)
    has_one(:report, Calcom.BookingReport, foreign_key: :booking_uid, references: :uid)
    has_many(:references, Calcom.BookingReference, foreign_key: :booking_id)
    has_many(:attendees, Calcom.Attendee, foreign_key: :booking_id)
    has_many(:payment, Calcom.Payment, foreign_key: :booking_id)
    has_many(:seats_references, Calcom.BookingSeat, foreign_key: :booking_id)
    has_many(:scheduled_triggers, Calcom.WebhookScheduledTriggers, foreign_key: :booking_id)
    has_many(:assignment_reason, Calcom.AssignmentReason, foreign_key: :booking_id)
    has_many(:internal_note, Calcom.BookingInternalNote, foreign_key: :booking_id)
    has_many(:expense_logs, Calcom.CreditExpenseLog, foreign_key: :booking_uid, references: :uid)
    has_many(:wrong_assignment_reports, Calcom.WrongAssignmentReport, foreign_key: :booking_uid, references: :uid)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end