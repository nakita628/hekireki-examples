defmodule Dub.Invoice do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          number: String.t() | nil,
          status: atom(),
          type: atom(),
          payment_method: atom() | nil,
          amount: integer(),
          fee: integer(),
          refunded_amount: integer(),
          total: integer(),
          receipt_url: String.t() | nil,
          refund_reason: String.t() | nil,
          failed_reason: String.t() | nil,
          registered_domains: map() | nil,
          stripe_charge_metadata: map() | nil,
          failed_attempts: integer(),
          paid_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          workspace: Dub.Project.t() | nil
        }

  schema "Invoice" do
    field(:id, :string, primary_key: true)
    field(:number, :string)
    field(:status, Ecto.Enum, values: [:processing, :completed, :failed], default: :processing)
    field(:type, Ecto.Enum, values: [:domainRenewal], default: :domainRenewal)
    field(:payment_method, Ecto.Enum, values: [:card, :ach, :ach_fast, :sepa, :acss], source: :paymentMethod)
    field(:amount, :integer, default: 0)
    field(:fee, :integer, default: 0)
    field(:refunded_amount, :integer, default: 0, source: :refundedAmount)
    field(:total, :integer, default: 0)
    field(:receipt_url, :string, source: :receiptUrl)
    field(:refund_reason, :string, source: :refundReason)
    field(:failed_reason, :string, source: :failedReason)
    field(:registered_domains, :map, source: :registeredDomains)
    field(:stripe_charge_metadata, :map, source: :stripeChargeMetadata)
    field(:failed_attempts, :integer, default: 0, source: :failedAttempts)
    field(:paid_at, Dub.PrismaDateTime, source: :paidAt)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Dub.Project, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end