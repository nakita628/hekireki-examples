defmodule Plunk.Project do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          public: String.t(),
          secret: String.t(),
          disabled: boolean(),
          disabled_reason: atom() | nil,
          customer: String.t() | nil,
          subscription: String.t() | nil,
          card_verification: atom() | nil,
          card_verification_intent: String.t() | nil,
          card_verification_at: DateTime.t() | nil,
          card_verification_session: String.t() | nil,
          card_verification_payment_method: String.t() | nil,
          card_verification_fingerprint: String.t() | nil,
          card_verification_attempts: integer(),
          billing_limit_workflows: integer() | nil,
          billing_limit_campaigns: integer() | nil,
          billing_limit_transactional: integer() | nil,
          billing_limit_inbound: integer() | nil,
          tracking: atom(),
          language: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          members: [Plunk.Membership.t()],
          contacts: [Plunk.Contact.t()],
          templates: [Plunk.Template.t()],
          segments: [Plunk.Segment.t()],
          workflows: [Plunk.Workflow.t()],
          campaigns: [Plunk.Campaign.t()],
          emails: [Plunk.Email.t()],
          events: [Plunk.Event.t()],
          domains: [Plunk.Domain.t()],
          api_requests: [Plunk.ApiRequest.t()],
          idempotency_keys: [Plunk.IdempotencyKey.t()],
          throttle_claims: [Plunk.ThrottleClaim.t()]
        }

  schema "projects" do
    field(:name, :string)
    field(:public, :string)
    field(:secret, :string)
    field(:disabled, :boolean, default: false)
    field(:disabled_reason, Ecto.Enum, values: [:PAYMENT_FAILED, :EMAIL_REPUTATION, :PHISHING_DETECTED, :CARD_VERIFICATION_FAILED, :CARD_VERIFICATION_REQUIRED, :MANUAL], source: :disabledReason)
    field(:customer, :string)
    field(:subscription, :string)
    field(:card_verification, Ecto.Enum, values: [:PENDING, :VERIFIED, :REQUIRES_ACTION, :FAILED], source: :cardVerification)
    field(:card_verification_intent, :string, source: :cardVerificationIntent)
    field(:card_verification_at, Plunk.PrismaDateTime, source: :cardVerificationAt)
    field(:card_verification_session, :string, source: :cardVerificationSession)
    field(:card_verification_payment_method, :string, source: :cardVerificationPaymentMethod)
    field(:card_verification_fingerprint, :string, source: :cardVerificationFingerprint)
    field(:card_verification_attempts, :integer, default: 0, source: :cardVerificationAttempts)
    field(:billing_limit_workflows, :integer, source: :billingLimitWorkflows)
    field(:billing_limit_campaigns, :integer, source: :billingLimitCampaigns)
    field(:billing_limit_transactional, :integer, source: :billingLimitTransactional)
    field(:billing_limit_inbound, :integer, source: :billingLimitInbound)
    field(:tracking, Ecto.Enum, values: [:ENABLED, :DISABLED, :MARKETING_ONLY], default: :ENABLED)
    field(:language, :string, default: "en")
    has_many(:members, Plunk.Membership, foreign_key: :project_id)
    has_many(:contacts, Plunk.Contact, foreign_key: :project_id)
    has_many(:templates, Plunk.Template, foreign_key: :project_id)
    has_many(:segments, Plunk.Segment, foreign_key: :project_id)
    has_many(:workflows, Plunk.Workflow, foreign_key: :project_id)
    has_many(:campaigns, Plunk.Campaign, foreign_key: :project_id)
    has_many(:emails, Plunk.Email, foreign_key: :project_id)
    has_many(:events, Plunk.Event, foreign_key: :project_id)
    has_many(:domains, Plunk.Domain, foreign_key: :project_id)
    has_many(:api_requests, Plunk.ApiRequest, foreign_key: :project_id)
    has_many(:idempotency_keys, Plunk.IdempotencyKey, foreign_key: :project_id)
    has_many(:throttle_claims, Plunk.ThrottleClaim, foreign_key: :project_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end