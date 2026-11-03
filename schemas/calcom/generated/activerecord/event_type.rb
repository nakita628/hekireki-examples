class EventType < ApplicationRecord
  self.table_name = "EventType"

  attribute :position, default: 0
  attribute :offsetStart, default: 0
  attribute :hidden, default: false
  attribute :useEventLevelSelectedCalendars, default: false
  attribute :lockTimeZoneToggleOnBookingPage, default: false
  attribute :requiresConfirmation, default: false
  attribute :requiresConfirmationWillBlockSlot, default: false
  attribute :requiresConfirmationForFreeEmail, default: false
  attribute :requiresBookerEmailVerification, default: false
  attribute :canSendCalVideoTranscriptionEmails, default: true
  attribute :autoTranslateDescriptionEnabled, default: false
  attribute :autoTranslateInstantMeetingTitleEnabled, default: true
  attribute :disableGuests, default: false
  attribute :hideCalendarNotes, default: false
  attribute :hideCalendarEventDetails, default: false
  attribute :minimumBookingNotice, default: 120
  attribute :beforeEventBuffer, default: 0
  attribute :afterEventBuffer, default: 0
  attribute :onlyShowFirstAvailableSlot, default: false
  attribute :showOptimizedSlots, default: false
  attribute :disableCancelling, default: false
  attribute :disableRescheduling, default: false
  attribute :seatsShowAttendees, default: false
  attribute :seatsShowAvailabilityCount, default: true
  attribute :allowReschedulingCancelledBookings, default: false
  attribute :price, default: 0
  attribute :currency, default: "usd"
  attribute :forwardParamsSuccessRedirect, default: true
  attribute :isInstantEvent, default: false
  attribute :instantMeetingExpiryTimeOffsetInSeconds, default: 90
  attribute :instantMeetingParameters, default: -> { [] }
  attribute :assignAllTeamMembers, default: false
  attribute :assignRRMembersUsingSegment, default: false
  attribute :useEventTypeDestinationCalendarEmail, default: false
  attribute :isRRWeightsEnabled, default: false
  attribute :includeNoShowInRRCalculation, default: false
  attribute :allowReschedulingPastBookings, default: false
  attribute :hideOrganizerEmail, default: false
  attribute :maxActiveBookingPerBookerOfferReschedule, default: false
  attribute :rescheduleWithSameRoundRobinHost, default: false
  attribute :useBookerTimezone, default: false
  attribute :bookingRequiresAuthentication, default: false
  attribute :rrHostSubsetEnabled, default: false
  attribute :enablePerHostLocations, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :periodType, { unlimited: "unlimited", rolling: "rolling", rolling_window: "rolling_window", range: "range" },
       default: :unlimited, validate: true
  enum :schedulingType, { round_robin: "roundRobin", collective: "collective", managed: "managed" },
       validate: { allow_nil: true }
  enum :requiresCancellationReason, { mandatory_both: "MANDATORY_BOTH", mandatory_host_only: "MANDATORY_HOST_ONLY", mandatory_attendee_only: "MANDATORY_ATTENDEE_ONLY", optional_both: "OPTIONAL_BOTH" },
       default: :mandatory_host_only, validate: { allow_nil: true }

  validates :title, presence: true
  validates :slug, presence: true, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }
  validates :slug, uniqueness: { scope: :teamId, conditions: -> { where.not(teamId: nil) } }
  validates :length, presence: true
  validates :userId, uniqueness: { scope: :parentId, conditions: -> { where.not(parentId: nil) }, allow_nil: true }

  belongs_to :owner, class_name: "User", foreign_key: "userId", optional: true, inverse_of: :owned_event_types
  belongs_to :profile, foreign_key: "profileId", optional: true, inverse_of: :event_types
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :event_types
  belongs_to :parent, class_name: "EventType", foreign_key: "parentId", optional: true, inverse_of: :children
  belongs_to :schedule, foreign_key: "scheduleId", optional: true, inverse_of: :event_type
  belongs_to :instant_meeting_schedule, class_name: "Schedule", foreign_key: "instantMeetingScheduleId", optional: true,
                                        inverse_of: :instant_meeting_events
  belongs_to :secondary_email, foreign_key: "secondaryEmailId", optional: true, inverse_of: :event_types
  belongs_to :restriction_schedule, class_name: "Schedule", foreign_key: "restrictionScheduleId", optional: true,
                                    inverse_of: :restriction_schedule
  has_one :destination_calendar, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :destroy
  has_one :cal_video_settings, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :destroy
  has_many :hosts, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :destroy
  has_many :hashed_link, class_name: "HashedLink", foreign_key: "eventTypeId", inverse_of: :event_type,
                         dependent: :destroy
  has_many :bookings, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :nullify
  has_many :availability, class_name: "Availability", foreign_key: "eventTypeId", inverse_of: :event_type,
                          dependent: :nullify
  has_many :webhooks, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :destroy
  has_many :custom_inputs, class_name: "EventTypeCustomInput", foreign_key: "eventTypeId", inverse_of: :event_type,
                           dependent: :destroy
  has_many :children, class_name: "EventType", foreign_key: "parentId", inverse_of: :parent, dependent: :destroy
  has_many :field_translations, class_name: "EventTypeTranslation", foreign_key: "eventTypeId", inverse_of: :event_type,
                                dependent: :destroy
  has_many :selected_calendars, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :nullify
  has_many :host_groups, foreign_key: "eventTypeId", inverse_of: :event_type, dependent: :destroy
  has_and_belongs_to_many :users, join_table: "_user_eventtype", foreign_key: "A", association_foreign_key: "B"
end
