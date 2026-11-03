defmodule Documenso.Recipient do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          email: String.t(),
          name: String.t(),
          token: String.t(),
          document_deleted_at: DateTime.t() | nil,
          expired: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          expiration_notified_at: DateTime.t() | nil,
          sent_at: DateTime.t() | nil,
          signed_at: DateTime.t() | nil,
          last_reminder_sent_at: DateTime.t() | nil,
          next_reminder_at: DateTime.t() | nil,
          reminder_count: integer(),
          auth_options: map() | nil,
          signing_order: integer() | nil,
          rejection_reason: String.t() | nil,
          role: atom(),
          read_status: atom(),
          signing_status: atom(),
          send_status: atom(),
          envelope: Documenso.Envelope.t() | nil,
          csc_credential: Documenso.CscCredential.t() | nil,
          csc_session: Documenso.CscSession.t() | nil,
          fields: [Documenso.Field.t()],
          signatures: [Documenso.Signature.t()]
        }

  schema "Recipient" do
    field(:email, :string)
    field(:name, :string, default: "")
    field(:token, :string)
    field(:document_deleted_at, Documenso.PrismaDateTime, source: :documentDeletedAt)
    field(:expired, Documenso.PrismaDateTime)
    field(:expires_at, Documenso.PrismaDateTime, source: :expiresAt)
    field(:expiration_notified_at, Documenso.PrismaDateTime, source: :expirationNotifiedAt)
    field(:sent_at, Documenso.PrismaDateTime, source: :sentAt)
    field(:signed_at, Documenso.PrismaDateTime, source: :signedAt)
    field(:last_reminder_sent_at, Documenso.PrismaDateTime, source: :lastReminderSentAt)
    field(:next_reminder_at, Documenso.PrismaDateTime, source: :nextReminderAt)
    field(:reminder_count, :integer, default: 0, source: :reminderCount)
    field(:auth_options, :map, source: :authOptions)
    field(:signing_order, :integer, source: :signingOrder)
    field(:rejection_reason, :string, source: :rejectionReason)
    field(:role, Ecto.Enum, values: [:CC, :SIGNER, :VIEWER, :APPROVER, :ASSISTANT], default: :SIGNER)
    field(:read_status, Ecto.Enum, values: [:NOT_OPENED, :OPENED], default: :NOT_OPENED, source: :readStatus)
    field(:signing_status, Ecto.Enum, values: [:NOT_SIGNED, :SIGNED, :REJECTED], default: :NOT_SIGNED, source: :signingStatus)
    field(:send_status, Ecto.Enum, values: [:NOT_SENT, :SENT], default: :NOT_SENT, source: :sendStatus)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    has_one(:csc_credential, Documenso.CscCredential, foreign_key: :recipient_id)
    has_one(:csc_session, Documenso.CscSession, foreign_key: :recipient_id)
    has_many(:fields, Documenso.Field, foreign_key: :recipient_id)
    has_many(:signatures, Documenso.Signature, foreign_key: :recipient_id)
  end
end