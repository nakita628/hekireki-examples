defmodule Calcom.EventType do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          title: String.t(),
          slug: String.t(),
          description: String.t() | nil,
          interface_language: String.t() | nil,
          position: integer(),
          locations: map() | nil,
          length: integer(),
          offset_start: integer(),
          hidden: boolean(),
          use_event_level_selected_calendars: boolean(),
          event_name: String.t() | nil,
          booking_fields: map() | nil,
          time_zone: String.t() | nil,
          period_type: atom(),
          period_start_date: DateTime.t() | nil,
          period_end_date: DateTime.t() | nil,
          period_days: integer() | nil,
          period_count_calendar_days: boolean() | nil,
          lock_time_zone_toggle_on_booking_page: boolean(),
          locked_time_zone: String.t() | nil,
          requires_confirmation: boolean(),
          requires_confirmation_will_block_slot: boolean(),
          requires_confirmation_for_free_email: boolean(),
          requires_booker_email_verification: boolean(),
          can_send_cal_video_transcription_emails: boolean(),
          auto_translate_description_enabled: boolean(),
          auto_translate_instant_meeting_title_enabled: boolean(),
          recurring_event: map() | nil,
          disable_guests: boolean(),
          hide_calendar_notes: boolean(),
          hide_calendar_event_details: boolean(),
          minimum_booking_notice: integer(),
          before_event_buffer: integer(),
          after_event_buffer: integer(),
          seats_per_time_slot: integer() | nil,
          only_show_first_available_slot: boolean(),
          show_optimized_slots: boolean() | nil,
          disable_cancelling: boolean() | nil,
          disable_rescheduling: boolean() | nil,
          minimum_reschedule_notice: integer() | nil,
          seats_show_attendees: boolean() | nil,
          seats_show_availability_count: boolean() | nil,
          scheduling_type: atom() | nil,
          allow_rescheduling_cancelled_bookings: boolean() | nil,
          price: integer(),
          currency: String.t(),
          slot_interval: integer() | nil,
          metadata: map() | nil,
          success_redirect_url: String.t() | nil,
          forward_params_success_redirect: boolean() | nil,
          booking_limits: map() | nil,
          duration_limits: map() | nil,
          is_instant_event: boolean(),
          instant_meeting_expiry_time_offset_in_seconds: integer(),
          instant_meeting_parameters: [String.t()],
          assign_all_team_members: boolean(),
          assign_rrmembers_using_segment: boolean(),
          rr_segment_query_value: map() | nil,
          use_event_type_destination_calendar_email: boolean(),
          is_rrweights_enabled: boolean(),
          max_lead_threshold: integer() | nil,
          include_no_show_in_rrcalculation: boolean(),
          allow_rescheduling_past_bookings: boolean(),
          hide_organizer_email: boolean(),
          max_active_bookings_per_booker: integer() | nil,
          max_active_booking_per_booker_offer_reschedule: boolean(),
          custom_reply_to_email: String.t() | nil,
          event_type_color: map() | nil,
          reschedule_with_same_round_robin_host: boolean(),
          use_booker_timezone: boolean(),
          booking_requires_authentication: boolean(),
          rr_host_subset_enabled: boolean(),
          requires_cancellation_reason: atom() | nil,
          enable_per_host_locations: boolean(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          owner: Calcom.User.t() | nil,
          profile: Calcom.Profile.t() | nil,
          team: Calcom.Team.t() | nil,
          parent: Calcom.EventType.t() | nil,
          schedule: Calcom.Schedule.t() | nil,
          instant_meeting_schedule: Calcom.Schedule.t() | nil,
          secondary_email: Calcom.SecondaryEmail.t() | nil,
          restriction_schedule: Calcom.Schedule.t() | nil,
          destination_calendar: Calcom.DestinationCalendar.t() | nil,
          cal_video_settings: Calcom.CalVideoSettings.t() | nil,
          hosts: [Calcom.Host.t()],
          hashed_link: [Calcom.HashedLink.t()],
          bookings: [Calcom.Booking.t()],
          availability: [Calcom.Availability.t()],
          webhooks: [Calcom.Webhook.t()],
          custom_inputs: [Calcom.EventTypeCustomInput.t()],
          children: [Calcom.EventType.t()],
          field_translations: [Calcom.EventTypeTranslation.t()],
          selected_calendars: [Calcom.SelectedCalendar.t()],
          host_groups: [Calcom.HostGroup.t()],
          users: [Calcom.User.t()]
        }

  schema "EventType" do
    field(:title, :string)
    field(:slug, :string)
    field(:description, :string)
    field(:interface_language, :string, source: :interfaceLanguage)
    field(:position, :integer, default: 0)
    field(:locations, :map)
    field(:length, :integer)
    field(:offset_start, :integer, default: 0, source: :offsetStart)
    field(:hidden, :boolean, default: false)
    field(:use_event_level_selected_calendars, :boolean, default: false, source: :useEventLevelSelectedCalendars)
    field(:event_name, :string, source: :eventName)
    field(:booking_fields, :map, source: :bookingFields)
    field(:time_zone, :string, source: :timeZone)
    field(:period_type, Ecto.Enum, values: [UNLIMITED: "unlimited", ROLLING: "rolling", ROLLING_WINDOW: "rolling_window", RANGE: "range"], default: :UNLIMITED, source: :periodType)
    field(:period_start_date, Calcom.PrismaDateTime, source: :periodStartDate)
    field(:period_end_date, Calcom.PrismaDateTime, source: :periodEndDate)
    field(:period_days, :integer, source: :periodDays)
    field(:period_count_calendar_days, :boolean, source: :periodCountCalendarDays)
    field(:lock_time_zone_toggle_on_booking_page, :boolean, default: false, source: :lockTimeZoneToggleOnBookingPage)
    field(:locked_time_zone, :string, source: :lockedTimeZone)
    field(:requires_confirmation, :boolean, default: false, source: :requiresConfirmation)
    field(:requires_confirmation_will_block_slot, :boolean, default: false, source: :requiresConfirmationWillBlockSlot)
    field(:requires_confirmation_for_free_email, :boolean, default: false, source: :requiresConfirmationForFreeEmail)
    field(:requires_booker_email_verification, :boolean, default: false, source: :requiresBookerEmailVerification)
    field(:can_send_cal_video_transcription_emails, :boolean, default: true, source: :canSendCalVideoTranscriptionEmails)
    field(:auto_translate_description_enabled, :boolean, default: false, source: :autoTranslateDescriptionEnabled)
    field(:auto_translate_instant_meeting_title_enabled, :boolean, default: true, source: :autoTranslateInstantMeetingTitleEnabled)
    field(:recurring_event, :map, source: :recurringEvent)
    field(:disable_guests, :boolean, default: false, source: :disableGuests)
    field(:hide_calendar_notes, :boolean, default: false, source: :hideCalendarNotes)
    field(:hide_calendar_event_details, :boolean, default: false, source: :hideCalendarEventDetails)
    field(:minimum_booking_notice, :integer, default: 120, source: :minimumBookingNotice)
    field(:before_event_buffer, :integer, default: 0, source: :beforeEventBuffer)
    field(:after_event_buffer, :integer, default: 0, source: :afterEventBuffer)
    field(:seats_per_time_slot, :integer, source: :seatsPerTimeSlot)
    field(:only_show_first_available_slot, :boolean, default: false, source: :onlyShowFirstAvailableSlot)
    field(:show_optimized_slots, :boolean, default: false, source: :showOptimizedSlots)
    field(:disable_cancelling, :boolean, default: false, source: :disableCancelling)
    field(:disable_rescheduling, :boolean, default: false, source: :disableRescheduling)
    field(:minimum_reschedule_notice, :integer, source: :minimumRescheduleNotice)
    field(:seats_show_attendees, :boolean, default: false, source: :seatsShowAttendees)
    field(:seats_show_availability_count, :boolean, default: true, source: :seatsShowAvailabilityCount)
    field(:scheduling_type, Ecto.Enum, values: [ROUND_ROBIN: "roundRobin", COLLECTIVE: "collective", MANAGED: "managed"], source: :schedulingType)
    field(:allow_rescheduling_cancelled_bookings, :boolean, default: false, source: :allowReschedulingCancelledBookings)
    field(:price, :integer, default: 0)
    field(:currency, :string, default: "usd")
    field(:slot_interval, :integer, source: :slotInterval)
    field(:metadata, :map)
    field(:success_redirect_url, :string, source: :successRedirectUrl)
    field(:forward_params_success_redirect, :boolean, default: true, source: :forwardParamsSuccessRedirect)
    field(:booking_limits, :map, source: :bookingLimits)
    field(:duration_limits, :map, source: :durationLimits)
    field(:is_instant_event, :boolean, default: false, source: :isInstantEvent)
    field(:instant_meeting_expiry_time_offset_in_seconds, :integer, default: 90, source: :instantMeetingExpiryTimeOffsetInSeconds)
    field(:instant_meeting_parameters, {:array, :string}, source: :instantMeetingParameters)
    field(:assign_all_team_members, :boolean, default: false, source: :assignAllTeamMembers)
    field(:assign_rrmembers_using_segment, :boolean, default: false, source: :assignRRMembersUsingSegment)
    field(:rr_segment_query_value, :map, source: :rrSegmentQueryValue)
    field(:use_event_type_destination_calendar_email, :boolean, default: false, source: :useEventTypeDestinationCalendarEmail)
    field(:is_rrweights_enabled, :boolean, default: false, source: :isRRWeightsEnabled)
    field(:max_lead_threshold, :integer, source: :maxLeadThreshold)
    field(:include_no_show_in_rrcalculation, :boolean, default: false, source: :includeNoShowInRRCalculation)
    field(:allow_rescheduling_past_bookings, :boolean, default: false, source: :allowReschedulingPastBookings)
    field(:hide_organizer_email, :boolean, default: false, source: :hideOrganizerEmail)
    field(:max_active_bookings_per_booker, :integer, source: :maxActiveBookingsPerBooker)
    field(:max_active_booking_per_booker_offer_reschedule, :boolean, default: false, source: :maxActiveBookingPerBookerOfferReschedule)
    field(:custom_reply_to_email, :string, source: :customReplyToEmail)
    field(:event_type_color, :map, source: :eventTypeColor)
    field(:reschedule_with_same_round_robin_host, :boolean, default: false, source: :rescheduleWithSameRoundRobinHost)
    field(:use_booker_timezone, :boolean, default: false, source: :useBookerTimezone)
    field(:booking_requires_authentication, :boolean, default: false, source: :bookingRequiresAuthentication)
    field(:rr_host_subset_enabled, :boolean, default: false, source: :rrHostSubsetEnabled)
    field(:requires_cancellation_reason, Ecto.Enum, values: [:MANDATORY_BOTH, :MANDATORY_HOST_ONLY, :MANDATORY_ATTENDEE_ONLY, :OPTIONAL_BOTH], default: :MANDATORY_HOST_ONLY, source: :requiresCancellationReason)
    field(:enable_per_host_locations, :boolean, default: false, source: :enablePerHostLocations)
    field(:user_id, :id, source: :userId)
    field(:profile_id, :id, source: :profileId)
    field(:team_id, :id, source: :teamId)
    field(:parent_id, :id, source: :parentId)
    field(:schedule_id, :id, source: :scheduleId)
    field(:instant_meeting_schedule_id, :id, source: :instantMeetingScheduleId)
    field(:secondary_email_id, :id, source: :secondaryEmailId)
    field(:restriction_schedule_id, :id, source: :restrictionScheduleId)
    belongs_to(:owner, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:profile, Calcom.Profile, foreign_key: :profile_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:parent, Calcom.EventType, foreign_key: :parent_id, define_field: false)
    belongs_to(:schedule, Calcom.Schedule, foreign_key: :schedule_id, define_field: false)
    belongs_to(:instant_meeting_schedule, Calcom.Schedule, foreign_key: :instant_meeting_schedule_id, define_field: false)
    belongs_to(:secondary_email, Calcom.SecondaryEmail, foreign_key: :secondary_email_id, define_field: false)
    belongs_to(:restriction_schedule, Calcom.Schedule, foreign_key: :restriction_schedule_id, define_field: false)
    has_one(:destination_calendar, Calcom.DestinationCalendar, foreign_key: :event_type_id)
    has_one(:cal_video_settings, Calcom.CalVideoSettings, foreign_key: :event_type_id)
    has_many(:hosts, Calcom.Host, foreign_key: :event_type_id)
    has_many(:hashed_link, Calcom.HashedLink, foreign_key: :event_type_id)
    has_many(:bookings, Calcom.Booking, foreign_key: :event_type_id)
    has_many(:availability, Calcom.Availability, foreign_key: :event_type_id)
    has_many(:webhooks, Calcom.Webhook, foreign_key: :event_type_id)
    has_many(:custom_inputs, Calcom.EventTypeCustomInput, foreign_key: :event_type_id)
    has_many(:children, Calcom.EventType, foreign_key: :parent_id)
    has_many(:field_translations, Calcom.EventTypeTranslation, foreign_key: :event_type_id)
    has_many(:selected_calendars, Calcom.SelectedCalendar, foreign_key: :event_type_id)
    has_many(:host_groups, Calcom.HostGroup, foreign_key: :event_type_id)
    many_to_many(:users, Calcom.User, join_through: "_user_eventtype", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end