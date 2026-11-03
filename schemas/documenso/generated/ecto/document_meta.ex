defmodule Documenso.DocumentMeta do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          subject: String.t() | nil,
          message: String.t() | nil,
          timezone: String.t() | nil,
          date_format: String.t() | nil,
          redirect_url: String.t() | nil,
          signing_order: atom(),
          allow_dictate_next_signer: boolean(),
          typed_signature_enabled: boolean(),
          upload_signature_enabled: boolean(),
          draw_signature_enabled: boolean(),
          language: String.t(),
          distribution_method: atom(),
          email_settings: map() | nil,
          email_reply_to: String.t() | nil,
          email_id: String.t() | nil,
          envelope_expiration_period: map() | nil,
          reminder_settings: map() | nil,
          envelope: Documenso.Envelope.t() | nil
        }

  schema "DocumentMeta" do
    field(:id, :string, primary_key: true)
    field(:subject, :string)
    field(:message, :string)
    field(:timezone, :string, default: "Etc/UTC")
    field(:date_format, :string, default: "yyyy-MM-dd hh:mm a", source: :dateFormat)
    field(:redirect_url, :string, source: :redirectUrl)
    field(:signing_order, Ecto.Enum, values: [:PARALLEL, :SEQUENTIAL], default: :PARALLEL, source: :signingOrder)
    field(:allow_dictate_next_signer, :boolean, default: false, source: :allowDictateNextSigner)
    field(:typed_signature_enabled, :boolean, default: true, source: :typedSignatureEnabled)
    field(:upload_signature_enabled, :boolean, default: true, source: :uploadSignatureEnabled)
    field(:draw_signature_enabled, :boolean, default: true, source: :drawSignatureEnabled)
    field(:language, :string, default: "en")
    field(:distribution_method, Ecto.Enum, values: [:EMAIL, :NONE], default: :EMAIL, source: :distributionMethod)
    field(:email_settings, :map, source: :emailSettings)
    field(:email_reply_to, :string, source: :emailReplyTo)
    field(:email_id, :string, source: :emailId)
    field(:envelope_expiration_period, :map, source: :envelopeExpirationPeriod)
    field(:reminder_settings, :map, source: :reminderSettings)
    has_one(:envelope, Documenso.Envelope, foreign_key: :document_meta_id)
  end
end