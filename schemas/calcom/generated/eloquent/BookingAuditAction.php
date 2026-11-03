<?php

namespace App\Models;

enum BookingAuditAction: string
{
    case CREATED = 'created';
    case CANCELLED = 'cancelled';
    case ACCEPTED = 'accepted';
    case REJECTED = 'rejected';
    case PENDING = 'pending';
    case AWAITING_HOST = 'awaiting_host';
    case RESCHEDULED = 'rescheduled';
    case ATTENDEE_ADDED = 'attendee_added';
    case ATTENDEE_REMOVED = 'attendee_removed';
    case REASSIGNMENT = 'reassignment';
    case LOCATION_CHANGED = 'location_changed';
    case NO_SHOW_UPDATED = 'no_show_updated';
    case RESCHEDULE_REQUESTED = 'reschedule_requested';
    case SEAT_BOOKED = 'seat_booked';
    case SEAT_RESCHEDULED = 'seat_rescheduled';
}